$VERSION = 'latest'
docker build -f "src\AddressValidator.Api\Dockerfile" --force-rm -t mrjamiebowman/addressvalidator:$VERSION --target base "src" 

docker images | findstr mrjamiebowman