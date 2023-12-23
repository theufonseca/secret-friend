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
                sh 'dotnet publish -c Release -o ./publish secret-friend-api/'
                sh 'cd publish'
                sh 'dotnet secret-friend-api.dll'
            }
        }
    }
}
