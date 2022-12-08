using SchoolWebApp1.Models;
using System.ComponentModel;

namespace SchoolWebApp1.ViewModels {
    public class GradesViewModel {
        public int Id { get; set; }
        [DisplayName("Číslo studenta")]
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string What { get; set; }
        public int Mark { get; set; }
        public DateTime Date { get; set; }
    }
}
