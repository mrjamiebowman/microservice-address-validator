$VERSION = 'latest'
docker build -f "src\AddressValidator.Api\Dockerfile" --no-cache --force-rm -t mrjamiebowman/addressvalidator:$VERSION --target base "src" 

docker images | findstr mrjamiebowman