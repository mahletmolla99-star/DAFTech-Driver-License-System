import 'package:http/http.dart' as http;
import 'dart:convert';

class ApiService {
 
static const String baseUrl = 'http://localhost:5173/api';


  static String? _token;

  static Future<Map<String, dynamic>> login(String username, String password) async {
    try {
      final response = await http.post(
        Uri.parse('$baseUrl/auth/login'),
        headers: {'Content-Type': 'application/json'},
        body: jsonEncode({
          'username': username,
          'password': password,
        }),
      );

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);
        _token = data['token'];
        return {'success': true, 'data': data};
      } else {
        return {'success': false, 'error': 'Login failed'};
      }
    } catch (e) {
      return {'success': false, 'error': e.toString()};
    }
  }

  static Future<Map<String, dynamic>> verifyLicense(String licenseId) async {
    try {
      final response = await http.post(
        Uri.parse('$baseUrl/verification/verify'),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer $_token',
        },
        body: jsonEncode({
          'licenseId': licenseId,
          'qrData': licenseId,
        }),
      );

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);
        return {
          'success': true,
          'status': data['status'],
          'licenseId': data['licenseId'],
          'fullName': data['fullName'],
          'expiryStatus': data['expiryStatus'],
        };
      } else {
        return {'success': false, 'error': 'Verification failed'};
      }
    } catch (e) {
      return {'success': false, 'error': e.toString()};
    }
  }


  static String? getToken() {
    return _token;
  }
    static Future<Map<String, dynamic>> registerDriver(
    String licenseId,
    String fullName,
    DateTime dateOfBirth,
    String licenseType,
    DateTime expiryDate,
    String qrRawData,
    String ocrRawText,
  ) async {
    try {
      final response = await http.post(
        Uri.parse('$baseUrl/driver/register'),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer $_token',
        },
        body: jsonEncode({
          'licenseId': licenseId,
          'fullName': fullName,
          'dateOfBirth': dateOfBirth.toIso8601String(),
          'licenseType': licenseType,
          'expiryDate': expiryDate.toIso8601String(),
          'qrRawData': qrRawData,
          'ocrRawText': ocrRawText,
        }),
      );

      if (response.statusCode == 200) {
        return {'success': true, 'data': jsonDecode(response.body)};
      } else {
        return {'success': false, 'error': 'Registration failed'};
      }
    } catch (e) {
      return {'success': false, 'error': e.toString()};
    }
  }
}