using System.Threading;

public class Pay
{
    public string payStatus = "wait";
    public int price = 0;

    private string phone = "";

    public void setPhone(string p)
    {
        phone = p;
    }

    public string getPhone()
    {
        return phone;
    }

    private Thread thread;

    public Pay(string phone, int price)
    {
        this.phone = phone;
        this.price = price;
        thread = new Thread(payThread);
        thread.Start();
    }

    private void payThread()
    {
        string payViaLoadParmResult = new SberTool().payViaLoadParm(price);
        payStatus = payViaLoadParmResult;
        if (!payViaLoadParmResult.Equals("0"))
        {
            switch (payViaLoadParmResult)
            {
                case "no":
                    payStatus = "wno";
                    break;
                case "":
                    payStatus = "empty";
                    break;
                case "4451":
                    payStatus = "not_enough_money";
                    break;
                case "2000":
                    payStatus = "user_cancel";
                    break;
                case "2002":
                    payStatus = "time_out";
                    break;
                case "4455":
                    payStatus = "incorrect_pin";
                    break;
                case "4475":
                    payStatus = "pin_was_blocked";
                    break;
            }

            if (payViaLoadParmResult.Contains("error"))
            {
                payStatus = "error";
            }
        }
        else
        {
            payStatus = "pay-ok";
            KKTDriverTool kktdt = new KKTDriverTool();
            int i = 0;
            foreach (var guest in SessionManager.getInstance().currentSession.basket.getGuests())
            {
                i++;
                Tiket t = new Tiket(i, guest);
                kktdt.printTiket(t);
            }

            if (kktdt.printFiscal(price, phone) == 1)
            {
                payStatus = "success";
            }
            else
            {
                payStatus = "unsuccessful";
            }
        }
    }
}