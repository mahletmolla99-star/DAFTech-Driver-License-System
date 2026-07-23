# DAFTech Driver License System - Screenshots

## System Overview
This document contains screenshots of the DAFTech Driver License Registration and Verification System demonstrating all key features.

## Screenshots

### 1. Login Screen (01_login.png)
- User login interface
- Username and password fields
- Login button for authentication

### 2. Home Screen (02_home.png)
- Welcome message to authenticated users
- Two main action buttons:
  - "Register Driver (Scan License)" - for driver registration
  - "Verify License" - for license verification

### 3. Driver Registration Screen (03_registration.png)
- License ID input field
- Full Name input field
- Date of Birth picker
- Expiry Date picker
- License Type input field
- "Register Driver" button to submit

### 4. License Verification Screen (04_verification.png)
- License ID input field
- "Verify License" button
- Verification result displaying:
  - Status (Real/Fake/Expired)
  - License ID
  - Full Name
  - Expiry Status
  - Verification message

## Features Demonstrated

1. **Authentication**: Secure login with JWT token
2. **Driver Registration**: Scan and register driver licenses with OCR/QR data
3. **License Verification**: Check license authenticity and expiry status
4. **Database Integration**: All data persisted in SQL Server
5. **REST API**: Backend API endpoints for all operations

## Technology Stack

- **Frontend**: Flutter (Web)
- **Backend**: ASP.NET Core (.NET 10)
- **Database**: SQL Server
- **Authentication**: JWT Bearer Tokens
