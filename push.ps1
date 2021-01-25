
# variables
$VERSION = 'latest'

Write-Host "[+] Deploying to Docker Hub"
Write-Host "Image Tag: " + $VERSION

docker build -f "src\AddressValidator.Api\Dockerfile" --force-rm -t mrjamiebowman/addressvalidator:$VERSION --target base "src" 

docker push mrjamiebowman/addressvalidator:$VERSION