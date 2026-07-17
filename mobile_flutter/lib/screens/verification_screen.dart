import 'package:flutter/material.dart';
import '../services/api_service.dart';

class VerificationScreen extends StatefulWidget {
  @override
  State<VerificationScreen> createState() => _VerificationScreenState();
}

class _VerificationScreenState extends State<VerificationScreen> {
  final licenseIdController = TextEditingController();
  bool isLoading = false;
  Map<String, dynamic>? verificationResult;

  void verifyLicense() async {
    if (licenseIdController.text.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Enter license ID')),
      );
      return;
    }

    setState(() => isLoading = true);

    final result = await ApiService.verifyLicense(licenseIdController.text);

    setState(() {
      isLoading = false;
      verificationResult = result;
    });
  }

  Color getStatusColor(String status) {
    switch (status.toLowerCase()) {
      case 'real':
        return Colors.green;
      case 'fake':
        return Colors.red;
      case 'expired':
        return Colors.orange;
      default:
        return Colors.grey;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Verify License')),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            TextField(
              controller: licenseIdController,
              decoration: const InputDecoration(
                labelText: 'Enter License ID',
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: isLoading ? null : verifyLicense,
              child: isLoading
                  ? const CircularProgressIndicator()
                  : const Text('Verify License'),
            ),
            const SizedBox(height: 24),
            if (verificationResult != null)
              Container(
                padding: const EdgeInsets.all(16),
                decoration: BoxDecoration(
                  border: Border.all(
                    color: getStatusColor(verificationResult!['status'] ?? ''),
                    width: 2,
                  ),
                  borderRadius: BorderRadius.circular(8),
                ),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      'Status: ${verificationResult!['status']}',
                      style: TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.bold,
                        color: getStatusColor(verificationResult!['status'] ?? ''),
                      ),
                    ),
                    const SizedBox(height: 8),
                    Text('License ID: ${verificationResult!['licenseId']}'),
                    Text('Full Name: ${verificationResult!['fullName']}'),
                    Text('Expiry Status: ${verificationResult!['expiryStatus']}'),
                  ],
                ),
              ),
          ],
        ),
      ),
    );
  }

  @override
  void dispose() {
    licenseIdController.dispose();
    super.dispose();
  }
}
