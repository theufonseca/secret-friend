pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                echo 'building...'
                sh 'dotnet build secret-friend-api/'
            }
        }
        
        stage('Test') {
            steps {
                echo 'testing...'
                sh 'dotnet test secret-friend-api/'
            }
        }
        
        stage('Deploy') {
            steps {
                echo 'deploying...'
                sh 'dotnet publish -c Release secret-friend-api/ -o ./publish'
                //sh 'cd ./publish && zip -r ../publish.zip .'
                sh 'cd ./publish && nohup dotnet secret-friend-api.dll --environment "Development" > output.log 2>&1 &'
            }
        }
    }
}
