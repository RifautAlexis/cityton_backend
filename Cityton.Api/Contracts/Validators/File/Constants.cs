using System.Collections.Generic;

namespace Cityton.Api.Contracts.Validators.File
{
    public static class FileConstants {
        public static readonly Dictionary<string, List<byte[]>> FileSignature = new Dictionary<string, List<byte[]>>
        {
            {
                "image/gif", new List<byte[]>
                {
                    new byte[] { 0x47, 0x49, 0x46, 0x38 },
                    new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 },
                    new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 },
                }

            },
            {
                "image/jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46, 0x49, 0x46, 0x00, 0x01 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    // new byte[] { 0xFF, 0xD8, 0xFF, 0xE1, 0x??, 0x??, 0x45, 0x78, 0x69, 0x66, 0x00, 0x00 },
                }
            },
            {
                "image/png", new List<byte[]>
                {
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A },
                }
            },
            // {
            //     "image/webp", new List<byte[]>
            //     {
            //         new byte[] { 0x52, 0x49, 0x46, 0x46, 0x??, 0x??, 0x??, 0x??, 0x57, 0x45, 0x42, 0x50 },
            //     }
            // },
            {
                "video/mpeg", new List<byte[]>
                {
                    new byte[] { 0x47 },
                    new byte[] { 0x00, 0x00, 0x01, 0xBA },
                    new byte[] { 0x00, 0x00, 0x01, 0xB3 },
                }
            },
            {
                "video/webm", new List<byte[]>
                {
                    new byte[] { 0x1A, 0x45, 0xDF, 0xA3 },
                }
            },
        };

        public static readonly List<string> ImageContentType = new List<string>
        {
            "image/gif",
            "image/jpeg",
            "image/png",
            // "image/webp",
        };

        public static readonly List<string> VideoContentType = new List<string>
        {
            "video/mpeg",
            "video/webm",
        };
    }
}