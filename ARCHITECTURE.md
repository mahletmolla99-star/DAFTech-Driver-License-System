# DAFTech Driver License System - Architecture

## System Overview

### Backend (.NET 10)
- REST API with clean architecture
- JWT-based authentication
- SQL Server database
- Three main services:
  - AuthenticationService - User login and token generation
  - DriverService - Driver registration and management
  - VerificationService - License verification and duplicate detection

### Frontend (Flutter)
- Cross-platform mobile app
- Login Screen - User authentication
- Home Screen - Navigation hub
- Camera Scan Screen - QR code and OCR scanning
- Driver Registration Screen - Submit driver data
- Verification Screen - Check license status

### Database Schema

**Users Table**
- UserId (PK)
- Username
- PasswordHash
- Status
- CreatedAt

**Drivers Table**
- DriverId (PK)
- LicenseId (Unique)
- FullName
- DateOfBirth
- LicenseType
- ExpiryDate
- QRRawData
- OCRRawText
- Status

**VerificationLogs Table**
- LogId (PK)
- LicenseId (FK)
- VerificationStatus (Real/Fake)
- VerifiedAt

### API Endpoints

**Authentication**
- POST /api/auth/login - User login

**Driver Management**
- POST /api/driver/register - Register new driver

**Verification**
- POST /api/verification/verify - Verify license

### Technology Stack
- Backend: .NET 10, Entity Framework Core
- Database: SQL Server
- Frontend: Flutter, Dart
- Authentication: JWT tokens
- Scanning: Mobile Scanner (QR), Google ML Kit (OCR)
