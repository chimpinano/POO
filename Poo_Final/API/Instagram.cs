using Newtonsoft.Json;
using Poo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Poo_Final.API
{
    public class Instagram
    {
        public List<Publication> GetPublications(string hashtag)
        {
            try
            {
                string url = String.Format
                  ("https://api.instagram.com/v1/tags/{0}/media/recent?client_id=9271d44b6abd4537b800da7347127757&count=20", hashtag);

                WebClient download = new WebClient();

                string result;

                try { result = download.DownloadString(url); }
                catch (Exception) { return null; }

                if (String.IsNullOrEmpty(result)) return null;

                var jsonReslt = JsonConvert.DeserializeObject<dynamic>(result);

                List<Publication> publications = new List<Publication>();

                foreach (var item in jsonReslt.data)
                {
                    Publication p = new Publication();

                    p.Id = item.id;
                    p.Image = item.images.standard_resolution.url;

                    try { p.Caption = item.caption.text; }
                    catch (Exception) { }

                    p.User = new User();
                    p.User.Username = item.user.username;
                    p.User.Id = item.user.id;
                    p.User.Image = item.user.profile_picture;

                    publications.Add(p);
                }

                return publications;
            }
            catch (Exception) { return null; }
        }

    }
}
