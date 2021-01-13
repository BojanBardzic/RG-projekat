#version 330 core
out vec4 FragColor;

struct Material {
    float shininess;
};

struct Light {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    vec3 position;

    float constant;
    float linear;
    float quadratic;
};

in vec2 TexCoord;
in vec3 Normal;
in vec3 FragPos;

uniform sampler2D texture1;
uniform sampler2D texture2;
uniform sampler2D textureSpecular;

uniform vec3 viewPos;
uniform Material material;
uniform Light light;


void main(){

    vec3 ambient = mix(texture(texture1, TexCoord).rgb, texture(texture2, TexCoord).rgb, 0.1) * light.ambient;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = mix(texture(texture1, TexCoord).rgb, texture(texture2, TexCoord).rgb, 0.1) * diff * light.diffuse;

    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = texture(textureSpecular, TexCoord).rgb * spec * light.specular;

    float distance = length(light.position - FragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * distance * distance);

    ambient *= attenuation;
    diffuse *= attenuation;
    specular *= attenuation;

    vec3 result = (ambient + diffuse + specular);
    FragColor = vec4(result, 1.0);
    //FragColor = vec4(result, 1.0f) * mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.1);
}