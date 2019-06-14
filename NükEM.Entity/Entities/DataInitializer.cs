using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NükEM.Entity.Entities
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<NükEMEntities>
    {
        protected override void Seed(NükEMEntities context)
        {
            

            var constants = new List<SalaryCalculationConstant>()
            {
                new SalaryCalculationConstant(){ Description="İşçi Malüllük, Yaşlılık, Ölüm Prim Oranı", SCCRatio=0.09, SCCCode="IYPO" },
                new SalaryCalculationConstant(){ Description="İşçi Kısa Vadeli Sigorta Prim Oranı", SCCRatio=0.0, SCCCode="IKVSPO" },
                new SalaryCalculationConstant(){ Description="İşçi Genel Sağlık Sigortası Prim Oranı", SCCRatio=0.05, SCCCode="IGSSPO" },
                new SalaryCalculationConstant(){ Description="İşçi İşsizlik Sigortası Oranı", SCCRatio=0.01, SCCCode="IISO" },
                new SalaryCalculationConstant(){ Description="İşçi Damga Vergisi Oranı", SCCRatio=0.00759, SCCCode="IDVO" }
              
            };

            foreach (var constant in constants)
            {
                context.SalaryCalculationConstants.Add(constant);
            }
            context.SaveChanges();

            var brackets = new List<TaxBracket>()
            {
                new TaxBracket(){ MinCITA=0, MaxCITA=18000, Bracket=0.15},
                new TaxBracket(){ MinCITA=18001, MaxCITA=40000, Bracket=0.20},
                new TaxBracket(){ MinCITA=40001, MaxCITA=148000, Bracket=0.27},
                new TaxBracket(){ MinCITA=148001,MaxCITA=9999999, Bracket=0.35}
            };

            foreach (var bracket in brackets)
            {
                context.TaxBrackets.Add(bracket);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
