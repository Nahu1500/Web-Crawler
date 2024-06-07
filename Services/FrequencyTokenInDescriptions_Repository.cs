using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Crawler.DAL;
using Web_Crawler.Models;

namespace Web_Crawler.Services
{
    public class FrequencyTokenInDescriptions_Repository
    {
        public int GetFrequencyOfToken(string token)
        {
            var word = new FrequencyTokenInDescriptions();
            using (var db = new DataBaseContext())
            {
                word = db.FrequencyTokenInDescriptions.FirstOrDefault(w => w.Token == token);
            }

            return word.Frequency;
        }

        public FrequencyTokenInDescriptions GetToken(string token)
        {
            using (var db = new DataBaseContext())
            {
                return db.FrequencyTokenInDescriptions.FirstOrDefault(word => word.Token == token);
            }
        }

        public bool TokenExists(string token)
        {
            using (var db = new DataBaseContext())
            {
                return db.FrequencyTokenInDescriptions.Any(word => word.Token == token);
            }
        }

        internal void AddToken(FrequencyTokenInDescriptions newToken)
        {
            using (var db = new DataBaseContext())
            {
                db.FrequencyTokenInDescriptions.Add(newToken);
                db.SaveChanges();
            }
        }

        internal void EditToken(FrequencyTokenInDescriptions editedToken)
        {
            using (var db = new DataBaseContext())
            {
                db.Entry(editedToken).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        internal void DeleteToken(string token)
        {
            using (var db = new DataBaseContext())
            {
                var tokenToRemove = db.FrequencyTokenInDescriptions.First(word => word.Token == token);
                db.FrequencyTokenInDescriptions.Remove(tokenToRemove);
                db.SaveChanges();
            }
        }
    }
}