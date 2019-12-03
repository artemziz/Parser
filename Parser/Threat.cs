using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Threat
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public string Source { get; set; }
        public string Subject { get; set; }
        public bool Confidentiality { get; set; }
        public bool Integrity { get; set; }
        public bool Availability { get; set; }
       

        public override string ToString()
        {
            return String.Format($"УБИ : {this.Id}\nНаименование УБИ : {this.Name}\nОписание : {this.Description}\nИсточник угрозы (характеристика и потенциал нарушителя) : {this.Source}\nОбъект воздействия : {this.Subject}\nНарушение конфиденциальности : {this.Confidentiality}\nНарушение целостности : {this.Integrity}\nНарушение доступности : {this.Availability}\n");
        }

        public override bool Equals(object  obj)
        {
            if (obj == null) return false;

            Threat t = obj as Threat;
            if (obj as Threat == null)  return false;

            if (this.Name == t.Name
            && this.Description == t.Description
            && this.Source == t.Source
            && this.Subject == t.Subject
            && this.Confidentiality == t.Confidentiality
            && this.Integrity == t.Integrity
            && this.Availability == t.Availability)
            {
                return true;

            }
            else return false;
            
        }
    }
}
