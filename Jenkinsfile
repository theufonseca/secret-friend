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
                echo 'dotnet publish...'
                sh 'dotnet publish -c Release -o ./publish secret-friend-api/'
                echo 'cd publish...'
                sh 'cd publish'
                echo 'dotnet run...'
                sh 'dotnet secret-friend-api.dll'
            }
        }
    }
}
