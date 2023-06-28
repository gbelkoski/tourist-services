# tourist-services

## Debug the solution
Use VS Code for the backend and VS 2022 for the mobile app
1. Check IP address on the dev machine
2. Set it up in the launch.json including the port.
   - Use https and port 5000
3.  Change the domain value in the Platforms/Android/Resoruces/xml/network_security_config.xml
4.  Change the TouristApi value in Constants.cs
5.  Run the backend
6.  Run the mobile on device
   
Troubleshoot: Make sure your device is on the same network as the dev machine

## Deploy the backend
To publish to linux server use the following steps:

1. Publish the app as self contained package
`dotnet publish "Tourist.Web.Api.csproj" -c Release --self-contained --runtime linux-x64`
2. Copy the bin\Release\net6.0\linux-x64\publish contents to the server app directory
3. Restart the kestrel server using
   `systemctl restart kestrel-cleanex.service`

## Deploy the android mobile app
1. Update the version and/or version code in AndroidManifest.xml and commit the changes
2. Run the following command `dotnet build -f net6.0-android`
3. Get the apk from  <app-root-dir>/bin/release/net6.0-android/com.companyname.appname-Signed.apk and upload it to appcenter