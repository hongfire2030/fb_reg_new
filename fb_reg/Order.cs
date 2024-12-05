using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    class Order
    {
        
        public static bool RunParallel ()
        {
            string value = GoogleSheet.GetValue(Constant.MANAGEMENT_SHEET, "C2:C2");
            Console.WriteLine("Run parallel:" + value);
            return value == "x";
        }

        public static ArrayList GetOrders()
        {
            var orders = new ArrayList();
            IList<IList<object>> values = GoogleSheet.GetValues(Constant.MANAGEMENT_SHEET, "A10:K");

            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    string rowString = "";
                    for (int i = 0; i < row.Count; i ++)
                    {
                        rowString = rowString + (string)row[i] + "|";
                    }
                    OrderObject order = ConvertString2Order(rowString);
                    if (order != null && order.status != "" && order.status != "Pending" && order.status !="Finish")
                    {
                        orders.Add(order);
                    }
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }

            return orders;
        }
        public static void UpdateStatus(OrderObject order, string status)
        {
            string range = "J" + order.index;
            
            GoogleSheet.UpdateEntry(Constant.MANAGEMENT_SHEET, range, status);
        }
        public static OrderObject ConvertString2Order(string row)
        {
            Console.WriteLine("ConvertString2Order - row:" + row);
            if (string.IsNullOrEmpty(row))
            {
                return null;
            }
            string[] rowArr = row.Split('|');
            OrderObject orderObject = new OrderObject();

            orderObject.index = Convert.ToInt32(rowArr[0]) + 9;
            orderObject.code = rowArr[1];
            orderObject.hasAvatar = rowArr[2] == "x";
            orderObject.hasCover = rowArr[3] == "x";
            orderObject.has2Fa = rowArr[4] == "x";
            
            orderObject.gender = rowArr[6];
            orderObject.isHotmail = rowArr[7] == "x";
            orderObject.amount = Convert.ToInt32(rowArr[8]);
            orderObject.status = rowArr[9];
            if (!string.IsNullOrEmpty(rowArr[10]))
            {
                orderObject.currentAmount = Convert.ToInt32(rowArr[10]);
            }
            
            return orderObject;
        }

    }
}
