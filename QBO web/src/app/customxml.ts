export function customXmlToJson(xmlString: string): any {
    const parser = new DOMParser();
    const xmlDoc = parser.parseFromString(xmlString, 'application/xml');
  
    function parseNode(node: Node): any {
      const result: any = {};
  
      if (node.nodeType === Node.ELEMENT_NODE) {
        const element = node as Element;
        const children = Array.from(element.childNodes);
        const attributes = element.attributes;
  
        // Add attributes to result
        if (attributes.length > 0) {
          result['@attributes'] = {};
          for (let i = 0; i < attributes.length; i++) {
            const attr = attributes[i];
            result['@attributes'][attr.name] = attr.value;
          }
        }
  
        // Process child nodes
        children.forEach(child => {
          if (child.nodeType === Node.ELEMENT_NODE) {
            const childElement = child as Element;
            const childName = childElement.nodeName;
            const childJson = parseNode(child);
            
            if (!result[childName]) {
              result[childName] = childJson;
            } else if (Array.isArray(result[childName])) {
              result[childName].push(childJson);
            } else {
              result[childName] = [result[childName], childJson];
            }
          } else if (child.nodeType === Node.TEXT_NODE) {
            const textContent = child.textContent?.trim();
            if (textContent) {
              result['#text'] = textContent;
            }
          }
        });
      }
  
      return result;
    }
  
    return parseNode(xmlDoc.documentElement);
  }
  