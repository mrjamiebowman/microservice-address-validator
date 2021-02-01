
$registry = "docker.io"
$username = ""
$password = ""

kubectl create secret docker-registry --dry-run=client dockercred --docker-server=$registry --docker-username=$username --docker-password=$password -o jsonpath="{.data.\.dockerconfigjson}"