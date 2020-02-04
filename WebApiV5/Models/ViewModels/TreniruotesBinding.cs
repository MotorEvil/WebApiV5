using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApiV5.Models
{
    public class JoinViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int Id { get; set; }
        public string UsersString{ get; set; }
        public int Joins { get; set; }




       /* public void Join_Click([Bind(Include = "Tid,TString,Join,UserId")] Treniruotes treniruotes)
        {
            Treniruotes treniruote = new Treniruotes()
            {
                Id = treniruotesString.Tid,
                Joins = treniruotesString.Join,
                UsersString = treniruotesString.TString
            };

            treniruote = db.Treniruotes.SingleOrDefault(x => x.Id == id);

            Users user = new Users()
            {
                Id = treniruotesString.UserId
            };

            user = db.Users.SingleOrDefault(x => x.Id == id);

            treniruote.UsersString = treniruote.UsersString + "," + treniruotesString.UserId.ToString();
            treniruote.Joins++;

            db.Entry(treniruote).State = EntityState.Modified;
            db.SaveChanges();
        }
*/
    }

   public class UserJoinViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
    }
}