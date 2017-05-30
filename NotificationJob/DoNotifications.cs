using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NotificationJob
{
    class DoNotifications
    {
        #region Properties

        public List<user_childs> AllChildrens { get; set; }
        public List<vaccination_check> AllVacChecks { get; set; }
        public List<vaccinations> AllVacs { get; set; }
        public List<vaccination_check> RelevantChecks { get; set; }

        public user_childs CurrentChild { get; set; }
        public users CurrentUser { get; set; }
        public vaccinations CurrentVaccination { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime VacDate { get; set; }

        const String ServerUrl = "http://vaccappwebservice20170424033719.azurewebsites.net/";


        private DBContext db = new DBContext();

        #endregion

        public DoNotifications()
        {
        }
        
        public void CreateLists()
        {
            AllChildrens = new List<user_childs>(db.user_childs);
            AllVacChecks = new List<vaccination_check>(db.vaccination_check);
            AllVacs = new List<vaccinations>(db.vaccinations);
            RelevantChecks = new List<vaccination_check>();
        }

        public void GetRelevantCases()
        {
            CreateLists();

            RelevantChecks = AllVacChecks.FindAll(x => (x.is_notification_sent == false) && (x.is_vac_done == false));
        }

        public void SendNotifications()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();

            if (RelevantChecks.Count != 0)
            {
                foreach (var RelVac in RelevantChecks)
                {
                    CurrentDate = DateTime.Now.Date;
                    CurrentChild = db.user_childs.Find(RelVac.child_id);
                    CurrentVaccination = db.vaccinations.Find(RelVac.vaccine_id);

                    if ((CurrentChild != null) && (CurrentVaccination != null))
                    {
                        VacDate = CurrentChild.birthday.Date.AddMonths(CurrentVaccination.injection_month - 1);

                        if ( CurrentDate >= VacDate)
                        {
                            CurrentUser = db.users.Find(CurrentChild.user_id);

                            var result = client.GetAsync($"SendNotification/{CurrentUser.email}/{CurrentUser.name}/{CurrentChild.name}/{VacDate.AddMonths(1).Date.ToString("MM-dd-yyyy")}/{CurrentVaccination.name}/").Result;

                            if (result.IsSuccessStatusCode)
                            {
                                var setVacCheck =
                                    db.vaccination_check.First(x => (x.child_id == CurrentChild.child_id) &&
                                                                    (x.vaccine_id == CurrentVaccination.vaccine_id));
                                setVacCheck.is_notification_sent = true;

                                db.vaccination_check.Attach(setVacCheck);
                                var entry = db.Entry(setVacCheck);
                                entry.Property(x => x.is_notification_sent).IsModified = true;
                                
                                db.SaveChanges();

                                Console.WriteLine("Email sendt");
                            }
                            else
                            {
                                Console.WriteLine("Fejl: " + result.StatusCode);
                            }
                        }
                    }
                }
            }
        }

    }
}
