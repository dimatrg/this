using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static int[] zn = { 1, 2, 5, 10 };//номиналы монет по возростанию
        class avt
        {
            private int[] km = { 0, 0, 0, 0 };//полученные монеты
            private int sum = 0;//сумма, на которую допустимы покупки
            private int[] pr = { 4, 3, 10 };//количество продуктов
            private int[] cen = { 50, 10, 30 };//цены
            private string[] us = { "куплен кекс", "куплено печенье", "куплена вафля" };
            private string[] ps = { "кексы", "печенья", "вафли" };
            public void pol(int nom, int kol)//получение нескольких монет определенного номинала
            {
                km[nom] = km[nom] + kol;
                sum = sum + kol * zn[nom];
            }
            public int sd(int nom)//выдача монет определенного номинала в количестве, не превышающем сумму счета
            {
                if (km[nom] * zn[nom] <= sum)//если сумма имеющихся монет не превышает счет
                {
                    int v = km[nom];
                    sum = sum - v * zn[nom];
                    km[nom] = 0;
                    if (v != 0)
                    {
                        Console.Write("передано " + v + " монет номиналом в " + zn[nom] + " рублей, ");
                    }
                    return v;
                }
                else
                {
                    int zd = sum / zn[nom];
                    km[nom] = km[nom] - zd;
                    sum = sum - zd * zn[nom];
                    if (zd != 0)
                    {
                        Console.Write("передано " + zd + " монет номиналом в " + zn[nom] + " рублей, ");
                    }
                    return zd;
                }
            }
            public void kpc(int nom)//попытка покупки определенного товара
            {
                if ((pr[nom] != 0) && (sum >= cen[nom]))
                {//удачная покупка
                    pr[nom]--;
                    sum = sum - cen[nom];
                    Console.WriteLine(us[nom]);
                }
                else
                {
                    if (sum >= cen[nom])
                    {
                        Console.WriteLine(ps[nom] + " кончились!");
                    }
                    else
                    {
                        Console.WriteLine("На счету недостаточно средств для выполнения данной покупки! Не хватает " + (cen[nom] - sum) + " рублей.");
                    }
                }
                Console.WriteLine("На счету " + sum + " рублей");
            }
            public void pr_s()//проверка полноты выдачи сдачи
            {
                if (sum != 0)
                {
                    Console.WriteLine("но не удается полностью выдать сдачу! На счету осталось " + sum + " рублей.");
                }
                else
                {
                    Console.WriteLine("и сдача выдана в полном объеме.");
                }
            }

        }
        class pok
        {
            private int sum = 150;//сумма в кошельке
            public Boolean if_phs(int m)//проверка имеющейся суммы
            {
                return m <= sum;
            }
            public void pol(int nom, int col)//получение нескольких монет определенного номинала
            {
                sum = sum + col;
            }
            public void vp(int s, avt a)//выплатa указанному автомату некой суммы
            {
                string ss = "";
                for (int i = 3; i >= 0; i--)//начиная с монет максимального номинала
                {
                    int sv = s - s % zn[i];//определение суммы, оплачиваемой монетами соответствующего номинала
                    s = s - sv;
                    sum = sum - sv;
                    sv = sv / zn[i];
                    a.pol(i, sv);
                    if (sv != 0)
                    {
                        if (ss != "")
                        {
                            ss = ss + ", ";
                        }
                        ss = ss + "передано " + sv + " монет номиналом в " + zn[i] + " рублей";
                    }
                }
                Console.WriteLine(ss + ".");
            }
        }
        static void Main(string[] args)
        {
            pok p = new pok();
            avt a = new avt();
            string s;
            s = Console.ReadLine();//считываем первую команду
            while (s != "end")
            {
                if (s[0] == '+')//s="+*необходимая сумма*"
                {
                    int sum = 0;//сумма
                    int j = 1;
                    while (s.Length > j)
                    {
                        sum = sum * 10 + (s[j] - '0');
                        j++;
                    }
                    if (p.if_phs(sum))//если денег хватает,
                    {
                        p.vp(sum, a);
                    }
                    else
                    {
                        Console.WriteLine("не хватает денег");
                    }
                }
                if (s[0] == 'b')
                {
                    int n = -1;
                    if (s[4] == 'c')//s="buy cupcakes"
                    {
                        n = 0;
                    }
                    if (s[4] == 'b')//s="buy biscuits"
                    {
                        n = 1;
                    }
                    if (s[4] == 'w')//s="buy wafers"
                    {
                        n = 2;
                    }
                    if (n != -1)
                    {
                        a.kpc(n);
                    }
                    else
                    {
                        Console.WriteLine("некорректная команда");
                    }
                }
                if (s[0] == 'd')//s="delivery"
                {
                    for (int i = 3; i >= 0; i--)//начиная с монет максимального номинала
                    {
                        p.pol(i, a.sd(i));//выдаем сумму частями
                    }
                    a.pr_s();//проверяем полноту сдачи
                }
                s = Console.ReadLine();//считываем следующую команду
            }
        }
    }
}
