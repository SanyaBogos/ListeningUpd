cd ../../src/Listening.Core
rm -rf bin obj

cd ../Listening.Infrastructure
rm -rf bin obj

cd ../Listening.Web
rm -rf bin obj

cd ClientApp
rm -rf node_modules
rm -rf dist

rm package-lock.json

cd ../../../tests/Infrastructure
rm -rf bin obj

cd ../Integration
rm -rf bin obj

cd ../Unit
rm -rf bin obj

cd ../../src/AdditionalProjects/EasyRTC
rm -rf node_modules
cd ../../../cmd/windows

npm cache clear --force
