import 'package:flutter/material.dart';
import 'driver_registration_screen.dart';

class CameraScanScreen extends StatefulWidget {
  @override
  State<CameraScanScreen> createState() => _CameraScanScreenState();
}

class _CameraScanScreenState extends State<CameraScanScreen> {
  final licenseIdController = TextEditingController();
  final ocrTextController = TextEditingController();

  @override
  void initState() {
    super.initState();
    licenseIdController.text = 'ETH-DL-2024-001';
    ocrTextController.text = 'John Doe, License Number: ETH-DL-2024-001';
  }

  void proceedToRegistration() {
    if (licenseIdController.text.isEmpty || ocrTextController.text.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Please fill in all fields')),
      );
      return;
    }

    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => DriverRegistrationScreen(
          qrData: licenseIdController.text,
          ocrText: ocrTextController.text,
        ),
      ),
    );
  }

  @override
  void dispose() {
    licenseIdController.dispose();
    ocrTextController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Scan or Enter License')),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            const Text('Camera not available on web. Enter manually:'),
            const SizedBox(height: 20),
            TextField(
              controller: licenseIdController,
              decoration: const InputDecoration(
                labelText: 'License ID (QR)',
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 16),
            TextField(
              controller: ocrTextController,
              maxLines: 3,
              decoration: const InputDecoration(
                labelText: 'OCR Text',
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 24),
            ElevatedButton(
              onPressed: proceedToRegistration,
              child: const Text('Proceed to Registration'),
            ),
          ],
        ),
      ),
    );
  }
}
