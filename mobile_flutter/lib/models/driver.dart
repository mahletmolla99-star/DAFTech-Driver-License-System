class Driver {
  final int driverId;
  final String licenseId;
  final String fullName;
  final DateTime dateOfBirth;
  final String licenseType;
  final DateTime expiryDate;
  final String qrRawData;
  final String ocrRawText;

  Driver({
    required this.driverId,
    required this.licenseId,
    required this.fullName,
    required this.dateOfBirth,
    required this.licenseType,
    required this.expiryDate,
    required this.qrRawData,
    required this.ocrRawText,
  });

  Map<String, dynamic> toJson() {
    return {
      'licenseId': licenseId,
      'fullName': fullName,
      'dateOfBirth': dateOfBirth.toIso8601String(),
      'licenseType': licenseType,
      'expiryDate': expiryDate.toIso8601String(),
      'qrRawData': qrRawData,
      'ocrRawText': ocrRawText,
    };
  }
}
