import 'package:flutter/material.dart';
import '../services/api_service.dart';

class DriverRegistrationScreen extends StatefulWidget {
  final String qrData;
  final String ocrText;

  const DriverRegistrationScreen({
    required this.qrData,
    required this.ocrText,
  });

  @override
  State<DriverRegistrationScreen> createState() =>
      _DriverRegistrationScreenState();
}

class _DriverRegistrationScreenState extends State<DriverRegistrationScreen> {
  final licenseIdController = TextEditingController();
  final fullNameController = TextEditingController();
  final licenseTypeController = TextEditingController();
  
  DateTime? dateOfBirth;
  DateTime? expiryDate;
  bool isLoading = false;

  @override
  void initState() {
    super.initState();
    // Pre-fill with scanned data
    licenseIdController.text = widget.qrData;
    fullNameController.text = widget.ocrText;
  }

  void register() async {
    if (dateOfBirth == null || expiryDate == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Please select both dates')),
      );
      return;
    }

    setState(() => isLoading = true);

    final result = await ApiService.registerDriver(
      licenseIdController.text,
      fullNameController.text,
      dateOfBirth!,
      licenseTypeController.text,
      expiryDate!,
      widget.qrData,
      widget.ocrText,
    );

    setState(() => isLoading = false);

    if (result['success']) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Driver registered successfully!')),
      );
      Navigator.pop(context);
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error: ${result['error']}')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Register Driver')),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            TextField(
              controller: licenseIdController,
              decoration: const InputDecoration(
                labelText: 'License ID',
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 16),
            TextField(
              controller: fullNameController,
              decoration: const InputDecoration(
                labelText: 'Full Name',
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () async {
                dateOfBirth = await showDatePicker(
                  context: context,
                  initialDate: DateTime(2000),
                  firstDate: DateTime(1950),
                  lastDate: DateTime.now(),
                );
                setState(() {});
              },
              child: Text(dateOfBirth == null 
                ? 'Select Date of Birth' 
                : 'DOB: ${dateOfBirth!.toLocal().toString().split(' ')[0]}'),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () async {
                expiryDate = await showDatePicker(
                  context: context,
                  initialDate: DateTime.now().add(const Duration(days: 365)),
                  firstDate: DateTime.now(),
                  lastDate: DateTime(2100),
                );
                setState(() {});
              },
              child: Text(expiryDate == null 
                ? 'Select Expiry Date' 
                : 'Expiry: ${expiryDate!.toLocal().toString().split(' ')[0]}'),
            ),
            const SizedBox(height: 16),
            TextField(
              controller: licenseTypeController,
              decoration: const InputDecoration(
                labelText: 'License Type (A, B, C, etc)',
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 24),
            SizedBox(
              width: double.infinity,
              child: ElevatedButton(
                onPressed: isLoading ? null : register,
                child: isLoading
                    ? const CircularProgressIndicator()
                    : const Text('Register Driver'),
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
    fullNameController.dispose();
    licenseTypeController.dispose();
    super.dispose();
  }
}
