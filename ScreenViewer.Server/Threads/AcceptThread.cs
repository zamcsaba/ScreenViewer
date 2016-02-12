using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ScreenViewer.Server.Threads
{
    public static class AcceptThread
    {
        public static void StartCommunication(Socket socket)
        {
            /*
             * ======================================================
             * 
             * KOMMUNIKÁCIÓ MENETE
             * 
             * ACCEPT
             * 1. Kliens -> Szerver     Kapcsolatfelvétel
             * 2. Kliens -> Szerver     Azonosító adatok hossza
             * 3. Kliens -> Szerver     Azonosító adatok
             * 4. Szerver               Azonosítás feldolgozása
             * 5. Kliens -> Szerver     Kérés / Parancs
             * 6. Szerver               Kérés / Parancs feldolgozása
             * 7. Szerver -> Kliens     Elfogadás / Elutasítás
             * 8. Kliens -> Szerver     Kérés / Parancs adatok hossza
             * 9. Kliens -> Szerver     Kérés / Parancs adatok
             * 10. Szerver              Adatok feldolgozása
             * 11. Szerver -> Kliens    Válasz hossza
             * 12. Szerver -> Kliens    Válasz
             * 13. Kliens -> Szerver    Bye
             * 14. Szerver -> Kliens    Bye
             * TERMINATE
             * 
             * ======================================================
             * 
             * AZONOSÍTÓ ADATOK
             * String: IP;FELHASZNÁLÓNÉV
             * 
             * KÉRÉS- / PARANCSTÍPUSOK
             * 1001                     KÉPET KÜLDÖK (LASSÚ)
             * 1002                     FOLYTONOS KÉPET KÜLDÖK (GYORS)
             * 1003                     
             * 
             * ELFOGADÁS / ELUTASÍTÁS
             * 200                      ELFOGADVA
             * 400                      ELUTASÍTVA
             * 
             * KÉRÉS / PARANCS ADATOK
             * byte[x]                  KÉP
             * 
             * VÁLASZ
             * 2000                     OK
             * 4000                     ERROR
             * 
             * BYE
             * 999                      BYE
             * 
             * TERMINATE
             * 9999                     TERMINATE
             * 
             * =======================================================
             * */

            //2 - Azonosító adatok hossza
            byte[] azonositoHossz = new byte[4];
            socket.Receive(azonositoHossz);

            //3 - Azonosító adatok
            byte[] azonositoAdatok = new byte[BitConverter.ToInt32(azonositoHossz, 0)];
            socket.Receive(azonositoAdatok);

            //4 - Azonosítás feldolgozása
            string ip;
            string felhasznalonev;
            string tmp = Utils.Utils.GetString(azonositoAdatok);
            ip = tmp.Trim().Split(';')[0];
            felhasznalonev = tmp.Trim().Split(';')[1];

            tmp = null;
            azonositoHossz = null;
            azonositoAdatok = null;

            //5 - Kérés / Parancs
            byte[] parancs_b = new byte[4];
            socket.Receive(parancs_b);

            //6 - Kérés / Parancs feldolgozása
            int parancs = BitConverter.ToInt32(parancs_b, 0);
            int elfogad = 400;
            if (parancs == 1)
            {
                elfogad = 200;
            }

            //7 - Elfogadás / Elutasítás
            byte[] elfogad_b = BitConverter.GetBytes(elfogad);
            socket.Send(elfogad_b);

            //8 - Kérés / Parancs adatok hossza
            byte[] parancs_adatok_hossz_b = new byte[4];
            socket.Receive(parancs_adatok_hossz_b);
            int parancs_adatok_hossz = BitConverter.ToInt32(parancs_adatok_hossz_b, 0);

            //9 - Kérés / Parancs adatok
            byte[] parancs_adatok = new byte[parancs_adatok_hossz];
            socket.Receive(parancs_adatok);

            //10 - Adatok feldolgozása
            //Nabazdmeg...
            int valasz = 2000;
            byte[] valasz_b = BitConverter.GetBytes(valasz);


            //11 - Válasz hossza
            byte[] valasz_hossz_b = BitConverter.GetBytes(valasz_b.Length);
            socket.Send(valasz_hossz_b);

            //12 - Válasz
            socket.Send(valasz_b);

            //13 - Bye
            byte[] bye = BitConverter.GetBytes(999);
            socket.Send(bye);

            //14 - Bye
            byte[] bye2 = new byte[4];
            socket.Receive(bye2);

            socket.Close(1);
        }
    }
}
