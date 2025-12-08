from asn1crypto import cms as asn1_cms
import base64
import zipfile
import os

COMMON_NAME = 'ImageMagick Studio LLC'

package = os.path.join('./packages', [file for file in os.listdir('./packages') if file.endswith('.nupkg')][0])

with zipfile.ZipFile(package, 'r') as zip_ref:
    signature_data = zip_ref.read('.signature.p7s')

certificates = asn1_cms.ContentInfo.load(signature_data)['content']['certificates']

for i, cert in enumerate(certificates):
    if cert.chosen['tbs_certificate']['subject'].native['common_name'] == COMMON_NAME:
        cert_b64 = base64.b64encode(cert.dump()).decode()
        print(f"-----BEGIN CERTIFICATE-----")
        print('\n'.join([cert_b64[j:j+64] for j in range(0, len(cert_b64), 64)]))
        print(f"-----END CERTIFICATE-----")
