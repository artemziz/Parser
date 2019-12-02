using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Threat
    {
        public int id { get; set; }
        public string name { get; set; } 
        public string description { get; set; }
        public string source { get; set; }
        public string subject { get; set; }
        public bool confidentiality { get; set; }
        public bool integrity { get; set; }
        public bool availability { get; set; }
        // public DateTime inclusionDate { get; set; } 
        // public DateTime dateOfChange { get; set; } 

        public override string ToString()
        {
            string threat;
            threat = String.Format($"УБИ : {this.id}\nНаименование УБИ : {this.name}\nОписание : {this.description}\nИсточник угрозы (характеристика и потенциал нарушителя) : {this.source}\nОбъект воздействия : {this.subject}\nНарушение конфиденциальности : {this.confidentiality}\nНарушение целостности : {this.integrity}\nНарушение доступности : {this.availability}\n");

            return threat;
        }
    }
}
