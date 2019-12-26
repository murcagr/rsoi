

echo "Deployment started"
kubectl apply -f cat.yml
kubectl apply -f owner.yml
kubectl apply -f food.yml
kubectl apply -f gw.yml
kubectl apply -f user.yml
kubectl apply -f Web.yml
echo "WOW! It works!"