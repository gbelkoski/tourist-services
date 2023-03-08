using System;

namespace Tourist.ShipmentMobile.Infrastructure;
public static class Constants
{
    public static readonly string DatabaseFilename = "CleanexDb.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    //public static string DatabasePath => Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, DatabaseFilename);

    //public static string DatabasePath => Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DatabaseFilename);

    //public static string DatabasePath => Path.Combine("/data/user/0/Documents", DatabaseFilename);

    //public static string DatabasePath => Path.Combine("/sdcard/Android/media/com.cleanex.shipmentmobile", DatabaseFilename);
}
