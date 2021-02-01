# create namespace for core microservices
kubectl create namespace corems

# install helm charts
helm install core-addressvalidator .\charts\addressvalidator\ -n corems