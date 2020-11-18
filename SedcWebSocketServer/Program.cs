using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SedcWebSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 669;
            var server = new TcpListener(IPAddress.Parse(ip), port);

            server.Start();
            Console.WriteLine("Server has started on {0}:{1}, Waiting for a connection...", ip, port);

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("A client connected.");

            NetworkStream stream = client.GetStream();

            // enter to an infinite cycle to be able to handle every change in stream
            while (true)
            {
                while (!stream.DataAvailable) ;
                while (client.Available < 3) ; // match against "get"

                byte[] bytes = new byte[client.Available];
                stream.Read(bytes, 0, client.Available);
                string s = Encoding.UTF8.GetString(bytes);

                if (Regex.IsMatch(s, "^GET", RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("=====Handshaking from client=====\n{0}", s);

                    // 1. Obtain the value of the "Sec-WebSocket-Key" request header without any leading or trailing whitespace
                    // 2. Concatenate it with "258EAFA5-E914-47DA-95CA-C5AB0DC85B11" (a special GUID specified by RFC 6455)
                    // 3. Compute SHA-1 and Base64 hash of the new value
                    // 4. Write the hash back as the value of "Sec-WebSocket-Accept" response header in an HTTP response
                    string swk = Regex.Match(s, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
                    string swka = swk + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                    byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(swka));
                    string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);

                    // HTTP/1.1 defines the sequence CR LF as the end-of-line marker
                    byte[] response = Encoding.UTF8.GetBytes(
                        "HTTP/1.1 101 Switching Protocols\r\n" +
                        "Connection: Upgrade\r\n" +
                        "Upgrade: websocket\r\n" +
                        "Sec-WebSocket-Accept: " + swkaSha1Base64 + "\r\n\r\n");

                    stream.Write(response, 0, response.Length);
                }
                else
                {

                    int fin = bytes[0] & 0b10000000;
                    if (fin == 0b10000000)
                    {
                        Console.WriteLine("This is a final fragment in the message");
                    } 
                    else
                    {
                        Console.WriteLine("This is NOT the final fragment in the message");
                    }

                    int opcode = bytes[0] & 0b00001111;

                    Console.Write("This message is a ");
                    Console.WriteLine(opcode switch {
                        0 => "continuation frame",
                        1 => "text message",
                        2 => "binary message",
                        8 => "connection close request",
                        9 => "ping message",
                        10 => "pong message",
                        _ => "UNKNOWN OPCODE"
                    });

                    int maskFlag = bytes[1] & 0b10000000;
                    bool hasMask;
                    int offset;
                    if (maskFlag == 0b10000000)
                    {
                        hasMask = true;
                        Console.WriteLine("The data is masked");
                    }
                    else
                    {
                        hasMask = false;
                        Console.WriteLine("The data is NOT masked");
                    }

                    int lenByte = bytes[1] & 0b01111111;
                    int length;

                    if (lenByte <= 125)
                    {
                        length = (int)lenByte;
                        offset = 2;
                    } 
                    else if (lenByte == 126)
                    {
                        length = (int)(bytes[2] * 256 + bytes[3]);
                        offset = 6;
                    } 
                    else
                    {
                        length = (int)BitConverter.ToUInt64(bytes[2..9], 0);
                        offset = 10;
                    }

                    byte[] masks = new byte[4];
                    
                    if (hasMask)
                    {
                        masks = new byte[4] { bytes[offset], bytes[offset + 1], bytes[offset + 2], bytes[offset + 3] };
                        offset += 4;
                        Console.WriteLine($"The mask is {string.Join(",", masks)}");
                    } 
                    else
                    {
                        Array.Fill<byte>(masks, 0);
                    }

                    byte[] payload = new byte[length];
                    Array.Copy(bytes, offset, payload, 0, length);

                    Console.WriteLine($"The payload is {string.Join(",", payload)}");

                    byte[] result = new byte[length];

                    for (int i = 0; i < length; i++)
                    {
                        result[i] =(byte) (payload[i] ^ masks[i % 4]);
                    }

                    if (opcode == 1)
                    {
                        var text = Encoding.ASCII.GetString(result);
                        Console.WriteLine(text);
                    }
                }
            }
        }
    }
}
