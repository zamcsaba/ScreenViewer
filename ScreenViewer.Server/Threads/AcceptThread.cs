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
             * BYE
             * 999                      BYE
             * 
             * TERMINATE
             * 9999                     TERMINATE
             * 
             * =======================================================
             * */

            //2
            byte[] azonositoHossz = new byte[4];
            socket.Receive(azonositoHossz);

            //3
            byte[] azonositoAdatok = new byte[BitConverter.ToInt32(azonositoHossz, 0)];
            socket.Receive(azonositoAdatok);

            //4
            string ip;
            string felhasznalonev;
            string tmp = Utils.Utils.GetString(azonositoAdatok);
            ip = tmp.Trim().Split(';')[0];
            felhasznalonev = tmp.Trim().Split(';')[1];

            tmp = null;
            azonositoHossz = null;
            azonositoAdatok = null;

            //5
            byte[] parancs_b = new byte[4];
            socket.Receive(parancs_b);

            //6
            int parancs = BitConverter.ToInt32(parancs_b, 0);

        }
    }
}
