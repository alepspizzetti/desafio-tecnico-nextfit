using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agendadorAulas.Model
{
    public class Plano
    {
        public int QtdAula { get; set; }

        public static int QtdAulaPorPlano(string plano)
        {
            return plano switch
            {
                "Mensal" => 1,
                "Trimestral" => 20,
                "Anual" => 30,
                _ => 0,
            };
        }
    }
}