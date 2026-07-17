import 'package:flutter/material.dart';
import 'package:mobile_scanner/mobile_scanner.dart';
import 'package:google_mlkit_text_recognition/google_mlkit_text_recognition.dart';
import 'driver_registration_screen.dart';

class CameraScanScreen extends StatefulWidget {
  @override
  State<CameraScanScreen> createState() => _CameraScanScreenState();
}

class _CameraScanScreenState extends State<CameraScanScreen> {
  late MobileScannerController scannerController;
  String qrData = '';
  String ocrText = '';
  bool isScanning = false;

  @override
  void initState() {
    super.initState();
    scannerController = MobileScannerController();
  }

  Future<void> extractOCR() async {
    setState(() {
      ocrText = 'OCR extracted from license';
    });
  }

  void handleQRScanned(Barcode barcode) async {
    if (!isScanning) {
      setState(() {
        isScanning = true;
        qrData = barcode.rawValue ?? '';
      });
      await extractOCR();
      
      if (mounted) {
        Navigator.push(
          context,
          MaterialPageRoute(
            builder: (context) => DriverRegistrationScreen(
              qrData: qrData,
              ocrText: ocrText,
            ),
          ),
        );
      }
    }
  }

  @override
  void dispose() {
    scannerController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Scan Driver License')),
      body: MobileScanner(
        controller: scannerController,
        onDetect: (capture) {
          final List<Barcode> barcodes = capture.barcodes;
          for (final barcode in barcodes) {
            handleQRScanned(barcode);
          }
        },
      ),
    );
  }
}
