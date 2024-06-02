using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH9329_control_csharp
{
    public static class keyboard
    {
        public static byte SHIFT = 0x80;
        public static readonly byte[] _asciimap = new byte[128]
        {
            0x00,             // NUL
            0x00,             // SOH
            0x00,             // STX
            0x00,             // ETX
            0x00,             // EOT
            0x00,             // ENQ
            0x00,             // ACK
            0x00,             // BEL
            0x2a,     // BS Backspace
            0x2b,     // TAB  Tab
            0x28,     // LF Enter
            0x00,             // VT
            0x00,             // FF
            0x00,             // CR
            0x00,             // SO
            0x00,             // SI
            0x00,             // DEL
            0x00,             // DC1
            0x00,             // DC2
            0x00,             // DC3
            0x00,             // DC4
            0x00,             // NAK
            0x00,             // SYN
            0x00,             // ETB
            0x00,             // CAN
            0x00,             // EM
            0x00,             // SUB
            0x00,             // ESC
            0x00,             // FS
            0x00,             // GS
            0x00,             // RS
            0x00,             // US
            0x2c,      //  ' '
            (byte)(0x1e | SHIFT),  // !
            (byte)(0x34 | SHIFT),  // "
            (byte)(0x20 | SHIFT),  // #
            (byte)(0x21 | SHIFT),  // $
            (byte)(0x22 | SHIFT),  // %
            (byte)(0x24 | SHIFT),  // &
            0x34,          // '
            (byte)(0x26 | SHIFT),  // (
            (byte)(0x27 | SHIFT),  // )
            (byte)(0x25 | SHIFT),  // *
            (byte)(0x2e | SHIFT),  // +
            0x36,          // ,
            0x2d,          // -
            0x37,          // .
            0x38,          // /
            0x27,          // 0
            0x1e,          // 1
            0x1f,          // 2
            0x20,          // 3
            0x21,          // 4
            0x22,          // 5
            0x23,          // 6
            0x24,          // 7
            0x25,          // 8
            0x26,          // 9
            (byte)(0x33 | SHIFT),    // :
            0x33,          // ;
            (byte)(0x36 | SHIFT),    // <
            0x2e,          // =
            (byte)(0x37 | SHIFT),    // >
            (byte)(0x38 | SHIFT),    // ?
            (byte)(0x1f | SHIFT),    // @
            (byte)(0x04 | SHIFT),    // A
            (byte)(0x05 | SHIFT),    // B
            (byte)(0x06 | SHIFT),    // C
            (byte)(0x07 | SHIFT),    // D
            (byte)(0x08 | SHIFT),    // E
            (byte)(0x09 | SHIFT),    // F
            (byte)(0x0a | SHIFT),    // G
            (byte)(0x0b | SHIFT),    // H
            (byte)(0x0c | SHIFT),    // I
            (byte)(0x0d | SHIFT),    // J
            (byte)(0x0e | SHIFT),    // K
            (byte)(0x0f | SHIFT),    // L
            (byte)(0x10 | SHIFT),    // M
            (byte)(0x11 | SHIFT),    // N
            (byte)(0x12 | SHIFT),    // O
            (byte)(0x13 | SHIFT),    // P
            (byte)(0x14 | SHIFT),    // Q
            (byte)(0x15 | SHIFT),    // R
            (byte)(0x16 | SHIFT),    // S
            (byte)(0x17 | SHIFT),    // T
            (byte)(0x18 | SHIFT),    // U
            (byte)(0x19 | SHIFT),    // V
            (byte)(0x1a | SHIFT),    // W
            (byte)(0x1b | SHIFT),    // X
            (byte)(0x1c | SHIFT),    // Y
            (byte)(0x1d | SHIFT),    // Z
            0x2f,          // [
            0x31,          // bslash
            0x30,          // ]
            (byte)(0x23 | SHIFT),  // ^
            (byte)(0x2d | SHIFT),  // _
            0x35,          // `
            0x04,          // a
            0x05,          // b
            0x06,          // c
            0x07,          // d
            0x08,          // e
            0x09,          // f
            0x0a,          // g
            0x0b,          // h
            0x0c,          // i
            0x0d,          // j
            0x0e,          // k
            0x0f,          // l
            0x10,          // m
            0x11,          // n
            0x12,          // o
            0x13,          // p
            0x14,          // q
            0x15,          // r
            0x16,          // s
            0x17,          // t
            0x18,          // u
            0x19,          // v
            0x1a,          // w
            0x1b,          // x
            0x1c,          // y
            0x1d,          // z
            (byte)(0x2f | SHIFT),  // {
            (byte)(0x31 | SHIFT),  // |
            (byte)(0x30 | SHIFT),  // }
            (byte)(0x35 | SHIFT),  // ~
            0       // DEL
        };
        public static byte[] press_keyboard(byte key)
        {
            byte[] result = new byte[14];
            byte modify_key = _asciimap[key];
            int sum = 0;
            result[0] = 0x57;
            result[1] = 0xAB;
            result[2] = 0x00;
            result[3] = 0x02;
            result[4] = 0x08;
            if ((_asciimap[key] & SHIFT) == SHIFT)
            {
                result[5] = 0x02;
                modify_key = (byte)(_asciimap[key] - SHIFT);
            }
            else
            {
                result[5] = 0x00;
            }
            result[6] = 0x00;
            result[7] = modify_key;
            result[8] = 0x00;
            result[9] = 0x00;
            result[10] = 0x00;
            result[11] = 0x00;
            result[12] = 0x00;
            for (int i = 0; i < 13; i++)
            {
                sum = sum + result[i];
            }
            result[13] = (byte)sum;
#if false
            Console.Write("byte[] : ");
            foreach (byte b in result)
            {
                Console.Write(b.ToString("X2") + " ");
            }
            Console.WriteLine();
#endif
            return result;
        }
        public static byte[] release_keyboard()
        {
            byte[] result = new byte[] { 0x57, 0xab, 0x00, 0x02, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0c };
            return result;
        }

        public static bool receive_check(byte[] recv)
        {
            byte[] right = new byte[] { 0x57, 0xab, 0x00, 0x82, 0x01, 0x00, 0x85};
            for(int i=0; i<7; i++)
            {
                if (right[i] != recv[i])
                {
                    return false;
                }
            } 
            return true;
        }
    }
}
