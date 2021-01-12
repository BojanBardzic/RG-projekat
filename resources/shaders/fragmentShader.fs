#version 330 core
out vec4 FragColor;

in vec2 TexCoord;
in vec3 Normal;
in vec3 FragPos;

uniform sampler2D texture1;
uniform sampler2D texture2;

//svetlo
uniform vec3 lightPos;
uniform vec3 lightColor;

void main(){

    float ambientStrenght = 0.2;
    vec3 ambient = ambientStrenght * lightColor;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;

    vec3 result = (ambient + diffuse);
    FragColor = vec4(result, 1.0f) * mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.1);
}