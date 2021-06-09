using System;
using System.Linq;
using System.Text.Json;
namespace d02_ex00.Model
{
    public class results
    {
        public String List_Name { get; set; }
        public int Rank { get; set; }
        book_details book_details = new book_details();
    }

    class book_details
    {
        public String Title { get; set; }
        public String Author { get; set; }
        public String description { get; set; }
        public String Url { get; set; }
    }
}
