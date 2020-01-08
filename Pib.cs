using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
namespace WpfApplication1
{

    [OracleCustomTypeMapping("SYSTEM.PIB")]
    class Pib : IOracleCustomType, INullable
    {

        String sql_type;


        [OracleObjectMappingAttribute("Pibb")]
        public int Pibb { get; set; }

        public bool IsNull => throw new NotImplementedException();

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "Pibb", this.Pibb);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            this.Pibb = ((int)(OracleUdt.GetValue(con, pUdt, "Pibb")));
        }


        public Pib()
        {
           // this.Pibb = ((int)(OracleUdt.GetValue(con, pUdt, "Pibb")));

            sql_type = "SYSTEM.PIB";
        }
    }
}
