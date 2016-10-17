using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgChartMvc.DAL
{
    [Table("Employees")]
    public class Employee
    {
        [Key] // Primary key
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string PhotoUrl { get; set; }
    }
}
