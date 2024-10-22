using IK.Domain.Entities.Abstract;
using IK.Domain.Entities.Identity;
using IK.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Domain.Entities.Concrete
{
    public class ExpenseRequest:Request
    {
        public ExpenseType ExpenseType { get; set; }
        public decimal ExpenseAmount { get; set; }

        public Currency ExpenseCurrency { get; set; }

        public byte[] Document {  get; set; }
        public virtual ICollection<AppUserExpenseRequest> AppUserExpenseRequests { get; set; }
    }
}
