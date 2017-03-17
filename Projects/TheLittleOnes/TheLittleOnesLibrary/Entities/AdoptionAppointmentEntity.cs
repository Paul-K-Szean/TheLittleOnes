using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleOnesLibrary.Entities
{
    public class AdoptionAppointmentEntity
    {
        private AccountEntity accountEntityFrom;
        private AdoptInfoEntity adoptInfoEntityTo;
        private DateTime appmtDateTime;

        public AdoptionAppointmentEntity(AccountEntity accountEntityFrom, AdoptInfoEntity adoptInfoEntityTo, DateTime appmtDateTime)
        {
            this.accountEntityFrom = accountEntityFrom;
            this.adoptInfoEntityTo = adoptInfoEntityTo;
            this.appmtDateTime = appmtDateTime;
        }

        public AccountEntity AccountEntityFrom { get => accountEntityFrom; set => accountEntityFrom = value; }
        public AdoptInfoEntity AdoptInfoEntityTo { get => adoptInfoEntityTo; set => adoptInfoEntityTo = value; }
        public DateTime AppmtDateTime { get => appmtDateTime; set => appmtDateTime = value; }
    }
}
