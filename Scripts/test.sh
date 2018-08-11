#! /bin/sh

echo "Attempting to run tests"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -runEditorTests \
  -batchmode \
  -nographics \
  -returnlicense \
  -forcefree \
  -username "mcgillparty@gmail.com" \
  -password "Ecse428!" \
  -silent-crashes \
  -projectPath $(pwd) \
  -logFile $(pwd)/unityTest.log \
test_return_code="$(echo $?)"

echo 'Logs from Test'
cat $(pwd)/unityTest.log

echo $test_return_code
