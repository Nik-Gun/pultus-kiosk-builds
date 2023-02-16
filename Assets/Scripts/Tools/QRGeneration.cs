using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

using System.Drawing.Imaging;
using UnityEngine;
using QRCoder;

public class QRGeneration : MonoBehaviour
{
    public INIWorkHelper INIWorkHelper;
    public static string QrPatch;
    // Start is called before the first frame update
    void Start()
    {
        QrPatch=INIWorkHelper.GetValue("settings.ini","qrFolder");
    }

 public static void QrGeg(string qr)
    {
     QRCodeGenerator qrGenerator = new QRCodeGenerator();
     QRCodeData qrCodeData = qrGenerator.CreateQrCode( qr,QRCodeGenerator.ECCLevel.Q);
     QRCode qrCode = new QRCode(qrCodeData);
     Bitmap qrCodeImage = qrCode.GetGraphic(20);
     MemoryStream ms= new MemoryStream();
     qrCodeImage.Save(QrPatch,ImageFormat.Bmp);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
