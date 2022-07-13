// See https://aka.ms/new-console-template for more information
using ConsoleAppDB;
using SimpleWebAPI.Domain;
using SampleWebAPI.Data;

/*Console.WriteLine("Hello, World!");

Student student1 = new Student();
student1.Nim = "78789988";
student1.Nama = "Anhar";
Console.WriteLine("nim:" + student1.Nim + " Nama : " + student1.Nama);
Console.WriteLine($"nim: { student1.Nim }   Nama : { student1.Nama}");

Lecturer lec1 = new Lecturer
{
    Nik ="1111",
    Nama ="Anhar",
    Alamat ="Jl. Hayu",
    Telp = "08908989080"

};*/
LecturerDAL lecturerDAL = new LecturerDAL();
//insert data
/*
var newlecturer = new Lecturer()
{
    Nik="3333",
    Nama="Huda",
    Alamat="Jl Haha",
    Telp="980980"
};

try
{
    lecturerDAL.Insert(newlecturer);
    Console.WriteLine($"Tambah data {newlecturer.Nama} Berhasil");
}
catch(Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}


var updateData = new Lecturer
{
    Nik = "3333",
    Nama = "Fitri",
    Alamat = "Jl Haha",
    Telp = "980980"
};
try
{
    lecturerDAL.Update(updateData);
    Console.WriteLine($"Update data dengan NIK - {updateData.Nik} Berhasil");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
*/

/*try
{
    lecturerDAL.Delete("3333");
    Console.WriteLine("Data Berhasil di delete");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

var data = lecturerDAL.GetAll();
foreach(var item in data)
{
    Console.WriteLine($"{item.Nik}-{item.Nama}-{item.Alamat}-{item.Telp}");
}*/

/*var data = lecturerDAL.GetByNama("r");
foreach (var item in data)
{
    Console.WriteLine($"{item.Nik}-{item.Nama}-{item.Alamat}-{item.Telp}");
}

var newlecturer = new Lecturer()
{
    Nik = "3333",
    Nama = "Huda",
    Alamat = "Jl Haha",
    Telp = "980980"
};

try
{
    lecturerDAL.Insert(newlecturer);
    Console.WriteLine($"Tambah data {newlecturer.Nama} Berhasil");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}*/
/*var singleData = lecturerDAL.GetById("1111");
        Console.WriteLine($"{singleData.Nik}-{singleData.Nama}-{singleData.Alamat}-{singleData.Telp}");
*/
/*var data = lecturerDAL.GetAll();
foreach (var item in data)
{
    Console.WriteLine($"{item.Nik}-{item.Nama}-{item.Alamat}-{item.Telp}");
}*/
/*
var data = lecturerDAL.GetByNama("r");
foreach (var item in data)
{
    Console.WriteLine($"{item.Nik}-{item.Nama}-{item.Alamat}-{item.Telp}");
}
*/

//Entity FrameWork


SamuraiContext _context = new SamuraiContext();
_context.Database.EnsureCreated();
Console.WriteLine("Sebelum Tambah Data");
GetSamurai();
Console.WriteLine("Tambah Data Samurai");
AddSamurai();
AddMultipleSamurai("Tanjiro","Zenitsu");
GetSamurai();

var data = GetById(2);
Console.WriteLine($"GetByid-{data.Id}-{data.Name}");
Console.ReadKey();

void AddSamurai()
{
    var samurai = new Samurai { Name = "Tanjiro", Title = "Pilar Matahari" };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();

}

void AddMultipleSamurai(params string[] names)
{
    foreach(string name in names) 
    { 
    _context.Samurais.Add(new Samurai { Name= names[0] });
    }
    _context.SaveChanges();
}

Samurai GetById(int id)
{
    var result = _context.Samurais.Where(s => s.Id == id).FirstOrDefault();
    if (result != null)
        return result;
    else
        throw new Exception($"Data tidak ditemukan");
}

void GetSamurai()
{
    var samurais = _context.Samurais.OrderByDescending(s=>s.Name).ToList();
    Console.WriteLine($"Jumlah Samurai : {samurais.Count}");
    foreach (var samurai in samurais)
    {
        Console.WriteLine(samurai.Name);

    }
}

_context.Database.EnsureDeleted();  









