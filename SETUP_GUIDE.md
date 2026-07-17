# DAFTech Driver License System - Setup Guide

## Prerequisites
- .NET 10 SDK
- Flutter SDK
- SQL Server (or SQL Server Express)
- Git

## Backend Setup

### 1. Clone and Navigate
git clone https://github.com/mahletmolla99-star/DAFTech-Driver-License-System.git
cd DAFTech-Driver-License-System/DAFTech.DriverLicenseSystem.Api

### 2. Database Setup
- Update connection string in appsettings.json
- Run migrations:
dotnet ef database update

### 3. Run Backend
dotnet run
Server will start on http://localhost:5173

### 4. Test User Credentials
- Username: testuser
- Password: password123

---

## Mobile (Flutter) Setup

### 1. Navigate to Mobile Directory
cd ../mobile_flutter

### 2. Install Dependencies
flutter pub get

### 3. Update API URL
- Edit lib/services/api_service.dart
- For local testing: http://localhost:5173/api
- For network: http://192.168.1.29:5173/api

### 4. Run on Chrome (Web Testing)
flutter run
Select Chrome when prompted

### 5. Build APK (Mobile)
flutter build apk --release

---

## Testing Flows

### Flow 1: Login
1. Enter username: testuser
2. Enter password: password123
3. Click Login

### Flow 2: Register Driver
1. Click "Register Driver"
2. Enter License ID: DL123456
3. Click "Proceed to Registration"
4. Fill in driver details
5. Click "Submit Registration"

### Flow 3: Verify License
1. Click "Verify License"
2. Enter License ID that was registered
3. View verification result (Real/Fake)

---

## Troubleshooting

### Issue: Connection timeout
- Ensure backend is running
- Check IP address in api_service.dart

### Issue: Camera not working on web
- QR scanning only works on Android/iOS
- Use text input for web testing

### Issue: Build errors
- Run flutter clean
- Run flutter pub get
- Try building again

---

## Next Steps
1. Install Android SDK for APK building
2. Test QR scanning on physical device
3. Configure production database
4. Deploy to app stores

