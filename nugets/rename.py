import os
import shutil

def rename(oldName, newName):
    to_delete = [
        #os.path.join(newName, '.vs'),
        os.path.join(newName, newName, 'bin'),
        os.path.join(newName, newName, 'obj'),
        os.path.join(newName, newName, newName + '.csproj.user')
    ]
    
    rename = [
        # directories
        (oldName, newName),
        (os.path.join(newName, oldName), os.path.join(newName, newName)),
        (os.path.join(newName, oldName + '.Tests'), os.path.join(newName, newName + '.Tests')),
        # files
        (os.path.join(newName, oldName + '.sln'), os.path.join(newName, newName + '.sln')),
        (os.path.join(newName, oldName + '.sln.DotSettings'), os.path.join(newName, newName + '.sln.DotSettings')),
        # proj files
        (os.path.join(newName, newName, oldName + '.csproj'), os.path.join(newName, newName, newName + '.csproj')),
        # proj tests files
        (os.path.join(newName, newName + '.Tests', oldName + '.Tests.csproj'), os.path.join(newName, newName + '.Tests', newName + '.Tests.csproj'))
    ]

    for oldPath, newPath in rename:
         if os.path.exists(oldPath):
             os.rename(oldPath, newPath)
         else:
             print('Path does not exist: ' + oldPath)

    for path in to_delete:         
        if os.path.exists(path):
            shutil.rmtree(path)
        else:
             print('Path does not exist: ' + path)
    
    with open(os.path.join(newName, newName + '.Tests', newName + '.Tests.csproj')) as file:
        content = file.read().replace(os.path.join(oldName, oldName + '.csproj'), os.path.join(newName, newName + '.csproj'))

    with open(os.path.join(newName, newName + '.Tests', newName + '.Tests.csproj'), 'w') as file:
        file.write(content)

    with open(os.path.join(newName, newName + '.sln')) as file:
        content = file.read().replace(oldName, newName)

    with open(os.path.join(newName, newName + '.sln'), 'w') as file:
        content = file.write(content)

    directories = [newName]
    while directories:
        next = directories.pop()
        for path in os.listdir(next):
            current = os.path.join(next, path)
            
            if os.path.isdir(current):
                directories.append(current)
            elif current[-3:].upper() == '.CS':
                with open(current) as file:
                    content = file.read().replace(oldName, newName)
                with open(current, 'w') as file:
                    content = file.write(content)
                

name = 'Md.TabletopGameAdmin.Common'           
renamed = 'Md.Tga.Common'

rename(name, renamed)

