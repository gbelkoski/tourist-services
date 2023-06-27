using System;

namespace Tourist.ShipmentMobile.Infrastructure;
public static class Constants
{
    public static readonly string DatabaseFilename = "CleanexDb.db3";

    public static string TouristApi = Preferences.Default.Get("ApiBaseUrl", "http://192.168.86.26:5000");

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}
