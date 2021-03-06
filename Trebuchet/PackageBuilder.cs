﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trebuchet
{
    class PackageBuilder
    {
        static byte[] header = {
	            0x01, 0x05, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00,
	            0x50, 0x61, 0x63, 0x6B, 0x61, 0x67, 0x65, 0x00, 0x00, 0x00, 0x00, 0x00,
	            0x00, 0x00, 0x00, 0x00
        };

        static byte[] trailingHeader = {
	        0x01, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        static byte[] BuildPackageList(string path, byte[] data)
        {
            //string file = Path.GetFileName(path) + "/\0";
            string file = "XXX\0";
            path = path + "/\0";

            MemoryStream stm = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stm);
            byte[] pathbytes = Encoding.ASCII.GetBytes(path);

            writer.Write((ushort)2);
            writer.Write(Encoding.ASCII.GetBytes(file));
            writer.Write(Encoding.ASCII.GetBytes("XXX\0"));
            writer.Write(0x30001);
            writer.Write(pathbytes.Length);
            writer.Write(pathbytes);
            writer.Write(data.Length);
            writer.Write(data);
            writer.Write((ushort)0);

            return stm.ToArray();
        }

        public static byte[] BuildPackage(string path, byte[] data)
        {
            MemoryStream stm = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stm);
            byte[] packageData = BuildPackageList(path, data);

            writer.Write(header);
            writer.Write(packageData.Length);
            writer.Write(packageData);
            writer.Write(trailingHeader);

            return stm.ToArray();
        }
    }
}
