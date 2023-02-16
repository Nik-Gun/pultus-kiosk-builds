using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using DrvFRLib;
using UnityEngine;

public class KKTDriverTool : MonoBehaviour
{
    int НомерПоследнейЛинии = 0;

    public bool printTiket(Tiket tiket)
    {
        DrvFR FR = new DrvFR();
        int Res = 0, PasswordAdmin = 30;
        //Connect
        FR.Password = PasswordAdmin;
        do
        {
            FR.Connect();

            Res = FR.ResultCode;
            if (Res > 0)
            {
                return false;
            }
            else if (Res == 0)
            {
                break;
            }

            else if (Res < 0)
            {
                return false;
            }
        } while (1 == 1);

        //печать текста
        PrintStr(FR, "360МАХ");
        PrintStr(FR, "Приготовьтесь почувствовать реальность");
        PrintStr(FR, "hello@360max.co / www.360max.co");
        PrintStr(FR, "Отсканируйте этот билет при входе в VR-кинотеатр, чтобы начать просмотр");
        PrintStr(FR, "/ в каждом билете фильмы только на одного зрителя / ");
        PrintStr(FR, "    ");
        PrintStr(FR, "Зритель N" + tiket.nn);
        PrintStr(FR, "    ");
        PrintStr(FR, "    ");

        //load and print image
        /*if (LoadPicture(FR, "name_file_image", false, false))
        {
            НомерПоследнейЛинии = 181;
            PrintPicture(FR, 1, НомерПоследнейЛинии);
        }*/
        PrintStr(FR, "ТУТ Будет QR");
        foreach (var product in tiket.guest.getProducts())
        {
            PrintStr(FR, product.film.names[product.language]);
        }


        //печать 1
        PrintStr(FR, "    ");
        PrintStr(FR, "ПРИЯТНОГО ПРОСМОТРА");

        //disconnect
        FR.Disconnect();
        return true;
    }

    public static bool printTest(int n)
    {
        DrvFR FR = new DrvFR();
        int Res = 0, PasswordAdmin = 30;
        //Connect
        FR.Password = PasswordAdmin;
        do
        {
            FR.Connect();

            Res = FR.ResultCode;
            if (Res > 0)
            {
                return false;
            }
            else if (Res == 0)
            {
                break;
            }

            else if (Res < 0)
            {
                return false;
            }
        } while (1 == 1);

        //печать текста
        PrintStr(FR, "360МАХ");
        PrintStr(FR, "Приготовьтесь почувствовать реальность");
        PrintStr(FR, "hello@360max.co / www.360max.co");
        PrintStr(FR, "Отсканируйте этот билет при входе в VR-кинотеатр, чтобы начать просмотр");
        PrintStr(FR, "/ в каждом билете фильмы только на одного зрителя / ");
        PrintStr(FR, "    ");
        PrintStr(FR, "Зритель N" + n);
        PrintStr(FR, "    ");
        PrintStr(FR, "    ");

        //load and print image
        /*if (LoadPicture(FR, "name_file_image", false, false))
        {
            НомерПоследнейЛинии = 181;
            PrintPicture(FR, 1, НомерПоследнейЛинии);
        }*/
        PrintStr(FR, "ТУТ Будет QR");
        PrintStr(FR, "    ");
        PrintStr(FR, "123");


        //печать 1
        PrintStr(FR, "    ");
        PrintStr(FR, "ПРИЯТНОГО ПРОСМОТРА");
        FR.CutType = false;
        FR.FeedAfterCut = true;
        FR.FeedLineCount = 10;
        FR.CutCheck();
        //disconnect
        FR.Disconnect();
        return true;
    }

    public int printFiscal(int price, string phone)
    {
        int result = 0;
        try
        {
            Process process = new Process();
            process.StartInfo.FileName = @"C:\\shtrih-m\\print_check.exe";
            process.StartInfo.Arguments = price + " " + phone;
            process.StartInfo.UseShellExecute = false;
            // process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            result = 1;
        }
        catch
            (Exception e)
        {
            result = -2;
        }

        return result;
    }

    public static bool PrintStr(DrvFR FR, string Str, int WideStr = 0, int NotPrintControl = 0, int ForMistake = 0)
    {
        int Res = 0, PasswordAdmin = 30;

        FR.Password = PasswordAdmin;

        FR.UseReceiptRibbon = true;
        if (NotPrintControl == 2)
        {
            FR.UseReceiptRibbon = false;
        }

        if (NotPrintControl == 1)
        {
            FR.UseJournalRibbon = false;
        }
        else
            FR.UseJournalRibbon = true;

        FR.StringForPrinting = Str;
        do
        {
            if (WideStr == 0)
            {
                FR.PrintString();
            }
            else
            {
                FR.FontType = WideStr;
                FR.PrintStringWithFont();
            }

            if (ForMistake == 1)
            {
                Res = 0;
            }
            else
                Res = FR.ResultCode;

            if (Res > 0)
            {
                return false;
            }
            else if (Res == 0)
                return true;
        } while (1 == 1);
    }

    bool LoadPicture(DrvFR FR, string NameFilePicture, bool CenterImage = true, bool ShowProgress = true)
    {
        int Res;

        НомерПоследнейЛинии = 0;
        int PasswordAdmin = 30;

        FR.Password = PasswordAdmin;
        FR.FileName = NameFilePicture;
        FR.CenterImage = CenterImage;
        FR.ShowProgress = ShowProgress;
        do
        {
            // FR.LoadImage();
            // НомерПоследнейЛинии = FR.LastLineNumber;
            FR.LoadLineDataEx();
            НомерПоследнейЛинии = FR.LastLineNumber;

            Res = FR.ResultCode;
            if (Res > 0)
            {
                FR.Disconnect();
                return false;
            }
            else if (Res == 0)
                return true;
        } while (1 == 1);
    }

    public bool PrintImage(String qrByteString, bool isHex)
    {
        DrvFR FR = new DrvFR();
        int Res = 0, PasswordAdmin = 30;
        // Connect
        FR.Password = PasswordAdmin;

        FR.Connect();

        // Res = FR.ResultCode;
        if (Res != 0)
        {
            return false;
        }

        if (isHex)
        {
            String[] lines = new String[512];
            for (int i = 0; i < 512; i++)
            {
                lines[i] = qrByteString.Substring(i, 64).TrimStart(' ');
            }

            // fill buffer
            FR.Password = PasswordAdmin;
            FR.GraphBufferType = 1;
            FR.LineLength = 64;
            for (int i = 0; i < 512; i++)
            {
                FR.FirstLineNumber = i + 1;
                FR.LineNumber = 1;
                FR.LineDataHex = lines[i];
                FR.LoadGraphics512();
            }

            // FR.FirstLineNumber = 1;
            // FR.LastLineNumber = 512;
            // FR.VertScale = 1;
            // FR.HorizScale = 1;
            // FR.DelayedPrint = false;
            // FR.UseSlipCheck = false;
            // draw image
            // FR.PrintGraphics512();
            FR.FirstLineNumber = 1;
            FR.LastLineNumber = 512;
            FR.DelayedPrint = false;
            FR.DrawEx();
        }
        else
        {
            // fill buffer
            FR.Password = PasswordAdmin;

            // for (int i = 0; i < 512; i++)
            // {
            //     FR.LineData = qrByteString.Substring(64*i, 64);
            //     FR.LineSwapBytes = false;
            //     FR.DelayedPrint = false;
            //     FR.PrintLine();
            // }

            FR.LineNumber = 1; // space + 2 chars per byte length
            FR.LineData = qrByteString;
            FR.WideLoadLineData();
            // draw image
            FR.FirstLineNumber = 1;
            FR.LastLineNumber = 512;
            FR.DrawEx();
        }

        FR.Password = PasswordAdmin;
        FR.PrintReportWithoutCleaning();
        // disconnect
        FR.Disconnect();
        return true;
    }


    bool PrintPicture(DrvFR FR, int FirstLineNumber, int LastLineNumber, bool Ex = false)
    {
        int Res, PasswordAdmin = 30;
        //print image

        FR.FirstLineNumber = FirstLineNumber;
        FR.LastLineNumber = LastLineNumber;
        FR.Password = PasswordAdmin;
        do
        {
            if (Ex)
                FR.DrawEx();
            else
                FR.Draw();

            Res = FR.ResultCode;
            if (Res > 0)
            {
                return false;
            }
            else if (Res == 0)
                return true;
        } while (1 == 1);
    }
}