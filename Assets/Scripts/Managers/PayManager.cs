using System.Collections.Generic;

public class PayManager
{
    private static PayManager instance;
    public Pay curPay;

    public static PayManager getInstance()
    {
        return instance ?? (instance = new PayManager());
    }

    // public string payStatus = "";

    // public int payStage = 0;

    PayManager()
    {
    }

    public Pay createPay(string phone,int price)
    {
        if (curPay == null || !curPay.payStatus.Equals("no"))
        {
            curPay = new Pay(phone,price);
        }

        return curPay;
    }

    public string update()
    {
        return curPay.payStatus;
    }
}