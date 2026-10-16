/****************
Mã sinh viên: 202418889
Họ tên: Đặng Thị Hương Giang

****************/
// Chương trình chính để test

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== LAB 06 Quàn lí mượn thiết bị ===");

        // 11.1 Luong thanh cong du 7 buoc
        BorrowingSlip phieu = null;
        try
        {
            // 1. Tao giang vien
            Lecturer gv = new Lecturer("GV01", "Thay A");
            Console.WriteLine("1. Tao giang vien GV01 ok");
            // 2. Tao 3 thiet bi
            ProjectorRemote dkMayChieu = new ProjectorRemote("TB01", "DK May chieu", 2022, 500000);
            AirConditionerRemote dkDieuHoa = new AirConditionerRemote("TB02", "DK Dieu hoa", 2021, 300000);
            WirelessMicro micro = new WirelessMicro("TB03", "Micro", 2023, 1000000);
            Console.WriteLine("2. Tao 3 thiet bi ok");
            // 3. Tao phieu cho phong
            ClassRoom phong = new ClassRoom("P101", "Phong 101", 50);
            phong.AddDevice(dkMayChieu);
            phong.AddDevice(dkDieuHoa);
            phong.AddDevice(micro);
            Console.WriteLine("3. Tao phong P101: " + phong.CalculateAnnualMaintenanceCost());
            phieu = new BorrowingSlip("PH01", gv, "P101", DateTime.Now);
            Console.WriteLine("Tao phieu PH01 ok");
            // 4. Them 2 thiet bi vao phieu
            phieu.AddDevice(dkMayChieu, 5);
            phieu.AddDevice(micro, 1);
            Console.WriteLine("4. Them may chieu + micro ok");
            // 5. Tra may chieu dung han
            decimal t1 = phieu.ReturnDevice("TB01", DateTime.Now.AddDays(5));
            Console.WriteLine("5. Tra may chieu dung han, phat: " + t1);
            // 6. Tra micro muon 2 ngay (qua han 1 ngay)
            decimal tien = phieu.ReturnDevice("TB03", DateTime.Now.AddDays(2));
            Console.WriteLine("6. Tra micro tre, phat: " + tien);
            // 7. Hien thi phieu tra muon
            Console.WriteLine("7. Tong tien tre: " + phieu.CalculateTotalLateFee());
            for (int i = 0; i < phieu.Items.Count; i++)
            {
                string st = phieu.Items[i].IsReturned ? "da tra" : "chua tra";
                Console.WriteLine("- " + phieu.Items[i].Device.MaThietBi + ": " + st);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Loi: " + ex.Message);
        }

        // 11.2 Luong ngoai le du 7 tinh huong
        Console.WriteLine("\n--- Test ngoai le 11.2 ---");
        // TH1: muon may UnderMaintenance
        try
        {
            TeachingAssistant tg = new TeachingAssistant("TG01", "Tro giang B");
            AirConditionerRemote dkHong = new AirConditionerRemote("TB10", "DK hong", 2020, 200000);
            dkHong.TrangThai = DeviceStatus.UnderMaintenance;
            BorrowingSlip p2 = new BorrowingSlip("PH02", tg, "P102", DateTime.Now);
            p2.AddDevice(dkHong, 2);
            Console.WriteLine("TH1 FAIL");
        }
        catch (DeviceUnavailableException ex)
        {
            Console.WriteLine("TH1 ok: " + ex.GetType().Name);
        }
        // TH2: tro giang muon 3 may
        try
        {
            TeachingAssistant tg2 = new TeachingAssistant("TG02", "TG C");
            BorrowingSlip p3 = new BorrowingSlip("PH03", tg2, "P103", DateTime.Now);
            p3.AddDevice(new ProjectorRemote("T1", "M1", 2022, 100000), 1);
            p3.AddDevice(new AirConditionerRemote("T2", "M2", 2022, 100000), 1);
            p3.AddDevice(new WirelessMicro("T3", "M3", 2022, 100000), 1);
            Console.WriteLine("TH2 FAIL");
        }
        catch (BorrowLimitExceededException ex)
        {
            Console.WriteLine("TH2 ok: " + ex.GetType().Name);
        }
        // TH3: them trung micro
        try
        {
            Lecturer gv3 = new Lecturer("GV03", "Thay C");
            BorrowingSlip p4 = new BorrowingSlip("PH04", gv3, "P104", DateTime.Now);
            WirelessMicro m2 = new WirelessMicro("TB11", "Micro 2", 2022, 800000);
            p4.AddDevice(m2, 1);
            p4.AddDevice(m2, 1);
            Console.WriteLine("TH3 FAIL");
        }
        catch (DuplicateDeviceException ex)
        {
            Console.WriteLine("TH3 ok: " + ex.GetType().Name);
        }
        // TH4: muon micro 3 ngay qua han 1 ngay
        try
        {
            Lecturer gv4 = new Lecturer("GV04", "Thay D");
            BorrowingSlip p5 = new BorrowingSlip("PH05", gv4, "P105", DateTime.Now);
            p5.AddDevice(new WirelessMicro("TB12", "Micro 3", 2022, 500000), 3);
            Console.WriteLine("TH4 FAIL");
        }
        catch (InvalidBorrowPeriodException ex)
        {
            Console.WriteLine("TH4 ok: " + ex.GetType().Name);
        }
        // TH5: tra may khong thuoc phieu
        try
        {
            Lecturer gv5 = new Lecturer("GV05", "Thay E");
            BorrowingSlip p6 = new BorrowingSlip("PH06", gv5, "P106", DateTime.Now);
            p6.AddDevice(new ProjectorRemote("TB20", "M", 2022, 100000), 2);
            p6.ReturnDevice("TB99", DateTime.Now);
            Console.WriteLine("TH5 FAIL");
        }
        catch (DeviceNotInSlipException ex)
        {
            Console.WriteLine("TH5 ok: " + ex.GetType().Name);
        }
        // TH6: tra 2 lan
        try
        {
            Lecturer gv6 = new Lecturer("GV06", "Thay F");
            BorrowingSlip p7 = new BorrowingSlip("PH07", gv6, "P107", DateTime.Now);
            ProjectorRemote mm = new ProjectorRemote("TB21", "M", 2022, 100000);
            p7.AddDevice(mm, 2);
            p7.ReturnDevice("TB21", DateTime.Now);
            p7.ReturnDevice("TB21", DateTime.Now);
            Console.WriteLine("TH6 FAIL");
        }
        catch (DeviceAlreadyReturnedException ex)
        {
            Console.WriteLine("TH6 ok: " + ex.GetType().Name);
        }
        // TH7: ngay tra truoc ngay muon
        try
        {
            Lecturer gv7 = new Lecturer("GV07", "Thay G");
            BorrowingSlip p8 = new BorrowingSlip("PH08", gv7, "P108", DateTime.Now);
            ProjectorRemote mm2 = new ProjectorRemote("TB22", "M", 2022, 100000);
            p8.AddDevice(mm2, 2);
            p8.ReturnDevice("TB22", DateTime.Now.AddDays(-5));
            Console.WriteLine("TH7 FAIL");
        }
        catch (Exception ex)
        {
            Console.WriteLine("TH7 ok: " + ex.GetType().Name);
        }
        finally
        {
            Console.WriteLine("Xong phan test (finally luon chay).");
        }
    }
}
